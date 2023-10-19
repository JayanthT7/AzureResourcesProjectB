import { Component } from '@angular/core';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent {

  constructor(private service: DataService) { }

  public contacts: any;
  ngOnInit() {
    this.service.getContacts().subscribe((data: any) => this.contacts = data);
  }

  DeleteContact(id: number) {
    this.service.deleteContact(id).subscribe((data: any) => {
      this.contacts = this.contacts.filter((x: any) => x.id !== id);
    });
  }

}
