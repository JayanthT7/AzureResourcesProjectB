import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getContacts() {
    return this.http.get(environment.apiUrl);
  }

  addContact(contact: any) {
    return this.http.post(environment.apiUrl, contact);
  }

  deleteContact(id: number) {
    return this.http.delete(`${environment.apiUrl}/${id}`);
  }
}
