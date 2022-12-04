import { BoardData } from "./boardData"

export interface InvitationData{
    id: any
    userEmail: string
    invitingEmail: string
    invitedAt: Date
    board: BoardData
}