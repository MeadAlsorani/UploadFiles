export interface FileToUploadDto {
  id: number;
  fileName: string;
  fileSize: number;
  fileType: string;
  uploadDate: Date;
}

export interface FileToUpload {
  FileType: string;
  FileName: string;
  FileDBName: string;
  Path: string;
  FileSize: number;
}
