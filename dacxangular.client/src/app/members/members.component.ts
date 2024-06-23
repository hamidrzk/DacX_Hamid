import { Component, OnInit  } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';
import { Router } from '@angular/router';
@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
  public members: Member[] = [];

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.getMembers();
  }

  getMembers() {
    this.http.get<Member[]>('/api/members').subscribe(
      (result) => {
        this.members = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  addNewMember() {
    this.router.navigateByUrl('/members-form');
  }
  deleteMember() {
    this.router.navigateByUrl('/members-delete');
  }
}
