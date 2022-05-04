export interface BaseResponse<T> {
  successful: boolean;
  errors: string[];
  data: T;
}
