'use client';

import { useEffect, useState } from 'react';
import { Button, InputNumber, Space, Typography, message } from 'antd';
import { Project } from '../../Models/Project';
import { projectService } from '../../Services/projectService';
import { LeftOutlined, PlusOutlined, RightOutlined } from '@ant-design/icons';
import { ProjectList } from './ProjectList';
import { CreateProjectModal } from './CreateProjectModal';
import { useTranslation } from 'react-i18next';
import { useRouter, useSearchParams } from 'next/navigation';

const { Title } = Typography;

export const Projects = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(true);
  const [totalPages, setTotalPages] = useState(10);
  
  const [createProjectModalOpen, setCreateModalOpen] = useState(false);
  const [fullProjectModalOpen, setFullModalOpen] = useState(false);
  
  const router = useRouter();
  const searchParams = useSearchParams();
  
  const urlPage = searchParams.get('pageNum');
  const initialPage = urlPage ? parseInt(urlPage, 10) : 1;
  const [currentPage, setCurrentPage] = useState(initialPage);

  const { t } = useTranslation('projects');

  const loadProjects = async (pageNum: number, pageSize: number) => {
    try {
      setLoading(true);
      const data = await projectService.getProjectsByPage(pageNum, pageSize);
      setProjects(data.projectResponses);
      setTotalPages(data.totalPages);
    } catch (error) {
      message.error(t('projects.errorLoading'));
      console.error('Error loading projects:', error);
    } finally {
      setLoading(false);
    }
  };

  const updateUrl = (pageNum: number) => {
    const params = new URLSearchParams(searchParams.toString());
    params.set('pageNum', pageNum.toString());
    router.push(`/projects?${params.toString()}`, { scroll: false });
  };

  useEffect(() => {
    loadProjects(currentPage, 6);
    updateUrl(currentPage);
  }, [currentPage]);

  const showCreateProjectModal = () => {
    setCreateModalOpen(true);
  };

  const handleCreateProjectModalClose = () => {
    setCreateModalOpen(false);
  };

  const handleProjectCreated = () => {
    handleCreateProjectModalClose();
    loadProjects(1, 6);
    setCurrentPage(1);
    updateUrl(1);
  };

  const handlePageChange = (value: number | null) => {
    if (value && value >= 1 && value <= totalPages) {
      setCurrentPage(value);
    }
  }; 
  
  const handleViewDetails = (project: Project) => {
    message.info(`${t('viewDetails')}: ${project.title}`);
    console.log('Project details:', project);
  };

  const prevPage = () => {
    if (currentPage > 1) {
      setCurrentPage(currentPage - 1);
    }
  };

  const nextPage = () => {
    if (currentPage < totalPages) {
      setCurrentPage(currentPage + 1);
    }
  };

  return (
    <div className="projects">
      <Space direction="vertical" style={{ width: '100%' }} size="large">
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <Title level={2}>{t('title')}</Title>
          <Button 
            type="primary" 
            icon={<PlusOutlined />}
            onClick={showCreateProjectModal}
          >
            {t('createProject')}
          </Button>
        </div>

        <ProjectList 
          projects={projects}
          loading={loading}
          onViewDetails={handleViewDetails}
        />
        
        <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
          <Button 
            icon={<LeftOutlined />}
            onClick={prevPage}
            disabled={currentPage === 1}
          />

          <Space.Compact>
            <InputNumber
              min={1}
              max={totalPages}
              size="small"
              value={currentPage}
              onChange={handlePageChange}
              controls={false}
              style={{ width: 60 }}
            />
            <span style={{ 
              padding: '0 8px', 
              display: 'flex', 
              alignItems: 'center', 
              background: '#fafafa',
              border: '1px solid #d9d9d9',
              borderLeft: 'none'
            }}>
              / {totalPages}
            </span>
          </Space.Compact>

          <Button 
            icon={<RightOutlined />}
            onClick={nextPage}
            disabled={currentPage === totalPages}
          />
        </div>
      </Space>

      <CreateProjectModal 
        open={createProjectModalOpen}
        onCancel={handleCreateProjectModalClose}
        onSuccess={handleProjectCreated}
      />
    </div>
  );
};