import { CommentData } from "./commentData";

export interface CommentResponse{
    data: CommentData[],
    code: number,
    errors: string[],
}