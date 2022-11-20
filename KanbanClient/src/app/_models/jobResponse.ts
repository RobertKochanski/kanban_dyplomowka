import { JobData } from "./jobData";

export interface JobResponse{
    data: JobData,
    code: number,
    errors: string[],
}