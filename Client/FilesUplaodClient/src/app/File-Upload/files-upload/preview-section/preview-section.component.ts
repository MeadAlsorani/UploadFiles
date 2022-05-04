import { environment } from './../../../../environments/environment.prod';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { BaseResponse } from './../../Models/BaseResponse';
import { Component, OnDestroy, OnInit, SecurityContext } from '@angular/core';
import { Subscription, tap } from 'rxjs';
import { FilesUploadService } from '../../Files-Upload.service';
import { FileToUploadDto } from '../../Models/FileToUpload';
import { ActivatedRoute, Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-preview-section',
  templateUrl: './preview-section.component.html',
  styleUrls: ['./preview-section.component.less'],
})
export class PreviewSectionComponent implements OnInit, OnDestroy {
  subs: Subscription[] = [];
  files: FileToUploadDto[] = [];
  tables: string[] = [];
  constructor(
    private uploadService: FilesUploadService,
    private notificationService: NzNotificationService,
    private sanitizer: DomSanitizer
  ) {}
  ngOnDestroy(): void {
    this.subs.forEach((sub) => sub.unsubscribe());
  }

  ngOnInit(): void {
    this.subscribeToUpload();
  }

  subscribeToUpload() {
    this.subs.push(
      this.uploadService.uploadFinished$.subscribe((res) => {
        const file = res.data as FileToUploadDto;
        this.files = [...this.files, file];
        this.getTables();
      })
    );
  }
  getTables() {
    // get unique tables
    this.files.forEach((file) => {
      if (!this.tables.includes(file.fileType)) {
        this.tables.push(file.fileType);
      }
    });
  }
  downloadFile(id: any) {
    this.uploadService
      .getFilePath(id)
      .pipe(
        tap((res: any) => {
          const path = res.fullPath;
          const securePath = this.sanitizer.sanitize(
            SecurityContext.RESOURCE_URL,
            this.sanitizer.bypassSecurityTrustResourceUrl(`${environment.BaseUrl}${path}`)
          ) as string;
          window.open(securePath, '_blank');
        })
      )
      .subscribe();
  }
  deleteFile(id: any) {
    this.uploadService.deleteFile(id).subscribe({
      next: (response: BaseResponse<boolean>) => {
        if (response.successful && response.data) {
          this.files = this.files.filter((file) => file.id !== id);
        }
      },
      error: (err) => {
        console.log(err);
        this.notificationService.error('Error', err.error.message);
      },
    });
  }
}
