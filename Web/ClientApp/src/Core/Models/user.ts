import { Role } from "./role";

export class User {
  Id: number;
  UserName: string;
  FirstName: string;
  LastName: string;
  DisplayName : string;
  Email: string;
  Password : string;
  Role: Role;
  RoleID: number;
}
