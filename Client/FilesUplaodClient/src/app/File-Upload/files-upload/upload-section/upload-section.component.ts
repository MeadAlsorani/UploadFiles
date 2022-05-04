import { FilesUploadService } from './../../Files-Upload.service';
import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs';
import { HttpEventType } from '@angular/common/http';
import { BaseResponse } from '../../Models/BaseResponse';
import { FileToUpload } from '../../Models/FileToUpload';
import { NzNotificationService } from 'ng-zorro-antd/notification';

@Component({
  selector: 'app-upload-section',
  templateUrl: './upload-section.component.html',
  styleUrls: ['./upload-section.component.less'],
})
export class UploadSectionComponent implements OnInit {
  progress = 0;
  message = 'Uploading...';
  constructor(
    private uploadService: FilesUploadService,
    private notificatonService: NzNotificationService
  ) {}

  ngOnInit(): void {}

  uploadFile(files: any) {
    if (files.length == 0) {
      return;
    }

    let fileTupUpload = files[0] as File;
    const formData = new FormData();
    formData.append('file', fileTupUpload, fileTupUpload.name);
    this.uploadService
      .uploadFile(formData)
      .pipe(
        tap((event) => {
          if (event.type === HttpEventType.UploadProgress)
            this.progress = Math.round((100 * event.loaded) / event.total);
          else if (event.type === HttpEventType.Response) {
            const response = event.body as BaseResponse<FileToUpload>;
            if (response.successful) {
              this.notificatonService.success(
                'Uploaded Successfully',
                'File Uploaded Successfully'
              );
              this.progress = 0;
              this.uploadService.uploadFinished.emit(event.body);
            } else {
              this.notificatonService.error(
                'Upload Failed',
                response.errors[0]
              );
            }
          }
          console.log(event);
        })
      )
      .subscribe();
  }
}
