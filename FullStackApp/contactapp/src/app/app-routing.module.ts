import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './contacts/contacts.component';
import { AddcontactComponent } from './addcontact/addcontact.component';

const routes: Routes = [
  { path: '', component: ContactsComponent },
  { path: 'add', component: AddcontactComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
