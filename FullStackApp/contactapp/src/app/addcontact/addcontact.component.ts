import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../services/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addcontact',
  templateUrl: './addcontact.component.html',
  styleUrls: ['./addcontact.component.css']
})
export class AddcontactComponent {
  constructor(private fb: FormBuilder, private service: DataService, private router: Router) { }

  public contactForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', Validators.required],
    age: ['', Validators.required],
    phone: ['', Validators.required],
    city: ['', Validators.required]
  })

  AddContact() {
    this.service.addContact(this.contactForm.value).subscribe(data => {
      this.router.navigateByUrl('/');
    });
  }

}
