import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { TokenService } from '../../../core/services/token.service';
import { ApiEndpointsService } from '../../../core/config/api-endpoints.service';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {
  private http = inject(HttpClient);
  private tokenService = inject(TokenService);
  private endPoints = inject(ApiEndpointsService);
}
