import { v4 as uuid } from 'uuid';
import { ColumnData } from './columnData';
import { UserData } from './userData';

export interface BoardData{
    id: string
    name: string
    columns: ColumnData[]
    ownerEmail: string
    members: UserData[]
}