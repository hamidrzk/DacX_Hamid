import { Member } from "./member"

export interface Tweet {
  "id": number
  "message": string,
  "memberId": number,
  "postDate": Date,
  "sender": Member
}
