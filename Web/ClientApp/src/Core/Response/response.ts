import { IResponse } from "./iresponse";
export class Response<T> implements IResponse<T>{
  isSuccess: boolean;
  message: string;
  data: T[]
}
