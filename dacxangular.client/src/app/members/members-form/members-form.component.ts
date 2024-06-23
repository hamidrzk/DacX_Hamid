import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { AbstractControl, FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Member } from '../../_models/member';


@Component({
  selector: 'app-members-form',
  templateUrl: './members-form.component.html',
  styleUrls: ['./members-form.component.css']
})

export class MembersFormComponent implements OnInit {

  //Create FormGroup
  memberForm: FormGroup = new FormGroup({
    name: new FormControl(''),
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
        name: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
      }
    );
  }

  alertText: string = '';
  submitted: boolean = false;

  model: Member = {
    "id": 0,
    "name": '',
    "email": ''
  };

  submitForm() {
    this.alertText = "";
    this.submitted = true;
    this.postMember();
  }

  ngOnInit(): void {
  }

  postMember() {
    console.log("this.model=" + this.model);
    this.http.post<Member>('/api/members/save', this.model).subscribe(
      (result) => {
        if (result) {
          this.alertText = "The new member is successfully added";
          setTimeout(() => {
            this.router.navigateByUrl('/members');
          }, 2000);
          //window.location.reload();
          
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

