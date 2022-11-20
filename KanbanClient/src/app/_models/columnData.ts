import { v4 as uuid } from 'uuid';
import { JobData } from './jobData';

export interface ColumnData{
    id: string
    boardId: string
    name: string
    jobs: JobData[],
}