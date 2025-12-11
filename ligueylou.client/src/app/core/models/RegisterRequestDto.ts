import { RoleEnum, SexeEnum } from "./user.enums";

export interface RegisterRequestDto {
  prenom: string,
  nom: string,
  email?: string,
  sexe?: SexeEnum,
  password: string,
  telephone?: string,
  role: RoleEnum
}
