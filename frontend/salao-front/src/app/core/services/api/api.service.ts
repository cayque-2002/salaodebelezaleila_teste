// api.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = 'https://localhost:7229'; // sua API

  constructor(private http: HttpClient) {}

  login(data: any) {
    return this.http.post(`${this.baseUrl}/login`, data);
  }

  getAgendamentos() {
    return this.http.get(`${this.baseUrl}/agendamento`);
  }

  createAgendamento(data: any) {
    return this.http.post(`${this.baseUrl}/agendamento`, data);
  }

  confirmarAgendamento(id: number, usuarioId: number) {
    return this.http.patch(
      `${this.baseUrl}/agendamento/${id}/confirmar?usuarioId=${usuarioId}`, 
      {}
    );
  }
}