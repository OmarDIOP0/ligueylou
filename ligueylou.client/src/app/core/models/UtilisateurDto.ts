import { RoleEnum, SexeEnum } from "./user.enums";

export interface UtilisateurDto {
  id: string;
  prenom: string;
  nom: string;
  email?: string;
  telephone?: string;
  role: RoleEnum;
  sexe?: SexeEnum;
  actif: boolean;
  dateCreation: Date;
  derniereConnexion?: Date;
}

export interface UpdateUtilisateurDto {
  prenom?: string;
  nom?: string;
  email?: string;
  telephone?: string;
  sexe?: SexeEnum;
}
