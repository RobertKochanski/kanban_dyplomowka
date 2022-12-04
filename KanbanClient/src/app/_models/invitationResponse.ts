import { InvitationData } from "./invitationData";

export interface InvitationReponse{
    data: InvitationData[],
    code: number,
    errors: string[],
}