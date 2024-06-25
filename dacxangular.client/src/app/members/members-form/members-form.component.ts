/* eslint-disable @typescript-eslint/no-explicit-any */
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { AbstractControl, FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Member } from '../../_models/member';
import { AccountService } from '../../_services/account.service';


@Component({
  selector: 'app-members-form',
  templateUrl: './members-form.component.html',
  styleUrls: ['./members-form.component.css']
})

export class MembersFormComponent implements OnInit {
  [x: string]: any;

  //Create FormGroup
  memberForm: FormGroup = new FormGroup({
    name: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(
    private accountService: AccountService,
    private formBuilder: FormBuilder,
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
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(16)
          ]
        ],
      }
    );
  }

  alertText: string = '';
  submitted: boolean = false;

  member: Member = {} as Member;

  submitForm() {
    this.alertText = "";
    this.submitted = true;
    this.save();
  }

  ngOnInit(): void {
    this.member = this.accountService.getCurrentUser();
  }

  save() {
    console.log("this.model=" + this.member);
    this.http.post<Member>('/api/members/save', this.member).subscribe(
      (result) => {
        if (result) {
          this.alertText = "The new member is successfully registered";
          setTimeout(() => {
            this.router.navigateByUrl('/tweets');
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

  cancel() {
    this.router.navigateByUrl('/');
  }
}

