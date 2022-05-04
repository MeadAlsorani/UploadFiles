import { FileUploadRoutingModule } from './FileUpload-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FilesUploadComponent } from './files-upload/files-upload.component';
import { PreviewSectionComponent } from './files-upload/preview-section/preview-section.component';
import { UploadSectionComponent } from './files-upload/upload-section/upload-section.component';
import { SharedPackagesModule } from '../Shared/SharedPackages/SharedPackages.module';
import { FilesUploadService } from './Files-Upload.service';

@NgModule({
  imports: [SharedPackagesModule, FileUploadRoutingModule],
  declarations: [
    FilesUploadComponent,
    PreviewSectionComponent,
    UploadSectionComponent,
  ],
  providers: [FilesUploadService],
})
export class FileUploadModule {}
