import { ColumnData } from "./columnData";

export interface ColumnResponse{
    data: ColumnData,
    code: number,
    errors: string[],
}