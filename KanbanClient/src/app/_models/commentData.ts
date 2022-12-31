import { JobData } from './jobData';

export interface CommentData{
    id: string
    text: string
    creator: string
    createAt: Date
    job: JobData,
}