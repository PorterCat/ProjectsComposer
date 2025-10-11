export interface Project {
  id: string;
  title: string;
  startDate: string;
  customerCompanyName?: string;
  contractorCompanyName?: string;
  endDate?: string;
  leaderId?: string;
}

export interface CreateProjectRequest {
  title: string;
  startDate: string;
  endDate?: string;
  customerCompanyName: string;
  contractorCompanyName: string;
  leaderId?: string;
}