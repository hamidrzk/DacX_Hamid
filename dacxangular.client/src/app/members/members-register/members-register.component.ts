/* eslint-disable @typescript-eslint/no-explicit-any */
import { Component, EventEmitter, Output } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Member } from '../../_models/member';


@Component({
  selector: 'app-members-register',
  templateUrl: './members-register.component.html',
  styleUrls: ['./members-register.component.css']
})

export class MembersRegisterComponent {

  @Output() cancelRegister = new EventEmitter();
  model: Member = {
    "id": 0,
    "name": '',
    "email": '',
    "password": '',
    "token":''
  };
  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: error => {
        this.toastr.error(error.error);
      }
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}

