import { UtilisateurDto } from "./UtilisateurDto";

export interface AuthResponse {
  token: string;
  refreshToken: string;
  tokenExpiresAt: Date;
  utilisateur: UtilisateurDto;
}
