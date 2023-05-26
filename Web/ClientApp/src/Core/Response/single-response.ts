import { IResponse } from "./iresponse";
export class SingleResponse<T> implements IResponse<T> {
  isSuccess: boolean;
  message: string;
  data: T
}
