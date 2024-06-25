/* eslint-disable prefer-const */
/* eslint-disable @typescript-eslint/no-explicit-any */
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/internal/operators/map';
import { BehaviorSubject } from 'rxjs';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'api/';

  private currentUserSource = new BehaviorSubject<Member | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: Member) {
    return this.http.post<Member>(this.baseUrl + 'account/login', model).pipe(
      map(
        (response: Member) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
          }
        }
      )
    )
  }

  register(model: any) {
    return this.http.post<Member>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: Member | null) {
    this.currentUserSource.next(user);
  }

  getCurrentUserEmail(): string {
    return this.getCurrentUser()?.email;
  }
  getCurrentUser(): Member {
    const user = localStorage.getItem('user');
    if (!user) {
      return {} as Member;
    }
    let member: Member = JSON.parse(user);
    return member;
  }

  logout() {
    localStorage.removeItem('user');
    this.setCurrentUser(null);
  }
}
