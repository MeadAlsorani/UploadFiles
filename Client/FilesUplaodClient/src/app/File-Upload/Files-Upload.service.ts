import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FileToUpload, FileToUploadDto } from './Models/FileToUpload';

@Injectable()
export class FilesUploadService {
  baseUrl = '';
  uploadFinished = new EventEmitter();
  constructor(private http: HttpClient) {
    this.baseUrl = environment.BaseUrl;
  }

  uploadFile(data: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}Files/UploadFile`, data, {
      reportProgress: true,
      observe: 'events',
    });
  }
  getFile(fileId: number): Observable<FileToUploadDto> {
    return this.http.get<FileToUploadDto>(`${this.baseUrl}Files/${fileId}`);
  }
  deleteFile(fileId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}Files/${fileId}`);
  }
}
