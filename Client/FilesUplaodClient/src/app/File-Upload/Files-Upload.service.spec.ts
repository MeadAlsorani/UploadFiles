/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FilesUploadService } from './Files-Upload.service';

describe('Service: FilesUpload', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FilesUploadService]
    });
  });

  it('should ...', inject([FilesUploadService], (service: FilesUploadService) => {
    expect(service).toBeTruthy();
  }));
});
