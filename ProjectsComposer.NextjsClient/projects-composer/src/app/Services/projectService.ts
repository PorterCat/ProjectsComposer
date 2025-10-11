import axios from 'axios';
import { Project, CreateProjectRequest } from '../Models/Project';

const API_BASE_URL = 'https://localhost:4000';

const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  }
});

export const projectService = {
  
  async getAllProjects(): Promise<Project[]> {
    const response = await api.get<Project[]>('/project/all');
    return response.data;
  },

  async getProjectById(id: string): Promise<Project> {
    const response = await api.get<Project>(`/project/${id}`);
    return response.data;
  },

  async createProject(projectData: CreateProjectRequest): Promise<Project> {
    const response = await api.post<Project>('/project', projectData);
    return response.data;
  },
};