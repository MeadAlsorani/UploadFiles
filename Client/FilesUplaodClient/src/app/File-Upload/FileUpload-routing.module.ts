import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FilesUploadComponent } from './files-upload/files-upload.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'file-upload',
    pathMatch: 'full',
  },
  {
    path: 'file-upload',
    component: FilesUploadComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FileUploadRoutingModule {}
