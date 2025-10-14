export interface Project {
  id: BigInt;
  title: string;
  startDate: Date;
  customerCompanyName?: string;
  contractorCompanyName?: string;
  endDate?: Date;
  leaderId?: BigInt;
}

export interface CreateProjectRequest {
  title: string;
  startDate: string;
  endDate?: string;
  customerCompanyName: string;
  contractorCompanyName: string;
  leaderId?: string;
}