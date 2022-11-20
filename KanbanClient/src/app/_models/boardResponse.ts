import { BoardData } from "./boardData";

export interface BoardResponse{
    data: BoardData,
    code: number,
    errors: string[],
}