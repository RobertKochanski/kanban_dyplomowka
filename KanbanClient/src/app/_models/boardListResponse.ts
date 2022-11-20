import { BoardData } from "./boardData";

export interface BoardListResponse{
    data: BoardData[],
    code: number,
    errors: string[],
}