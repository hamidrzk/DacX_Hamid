import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { AbstractControl, FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Member } from '../../_models/member';

@Component({
  selector: 'app-members-delete',
  templateUrl: './members-delete.component.html',
  styleUrls: ['./members-delete.component.css']
})


export class MembersDeleteComponent implements OnInit {

  //Create FormGroup
  memberForm: FormGroup = new FormGroup({
    email: new FormControl(''),
  });

  constructor(private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router) {
    this.initForm();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.memberForm.controls;
  }

  initForm() {
    this.memberForm = this.formBuilder.group(
      {
        email: ['', [Validators.required, Validators.email]],
      }
    );
  }

  memberEmail: string = '';
  alertText: string = '';
  submitted: boolean = false;

  submitForm() {
    this.alertText = "";
    this.submitted = true;
    this.deleteMember();
  }

  ngOnInit(): void {
  }

  deleteMember() {
    const member: Member = {} as Member;

    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: member
    };

    this.http.delete<string>('/api/members/deleteByEmail', options).subscribe(
      (result) => {
        if (result) {
          this.alertText = "The new member is successfully deleted";
          setTimeout(() => {
            this.router.navigateByUrl('/members');
          }, 2000);
        }
        else {
          this.alertText = result;
        }
      },
      (ex) => {
        this.alertText = ex.error;
        console.error(ex);
        this.submitted = false;
      }
    );
  }
}




