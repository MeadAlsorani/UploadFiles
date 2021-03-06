import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { HttpClientModule } from '@angular/common/http';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzNotificationModule } from 'ng-zorro-antd/notification';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzProgressModule } from 'ng-zorro-antd/progress';

@NgModule({
  imports: [],
  exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    NzLayoutModule,
    NzGridModule,
    NzMessageModule,
    NzNotificationModule,
    NzTableModule,
    NzSpaceModule,
    NzProgressModule,
  ],
})
export class SharedPackagesModule {}
