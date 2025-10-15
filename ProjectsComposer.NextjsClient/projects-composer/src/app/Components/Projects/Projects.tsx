'use client';

import { useEffect, useState } from 'react';
import { Button, InputNumber, Space, Typography, message } from 'antd';
import { Project } from '../../Models/Project';
import { projectService } from '../../Services/projectService';
import { LeftOutlined, PlusOutlined, RightOutlined } from '@ant-design/icons';
import { ProjectList } from './ProjectList';
import { CreateProjectModal } from './CreateProjectModal';
import { useTranslation } from 'react-i18next';

const { Title } = Typography;

export const Projects = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(true);
  
  const [createProjectModalOpen, setCreateModalOpen] = useState(false);
  const [fullProjectModalOpen, setFullModalOpen] = useState(false);
  
  const { t } = useTranslation('projects');

  const loadProjects = async (pageNum: number, pageSize: number) => {
    try {
      setLoading(true);
      const data = await projectService.getPage(pageNum, pageSize);
      setProjects(data);
    } catch (error) {
      message.error(t('projects.errorLoading'));
      console.error('Error loading projects:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadProjects(1, 6); // Check it later
  }, []);

  const showCreateProjectModal = () => {
    setCreateModalOpen(true);
  };

  const handleCreateProjectModalClose = () => {
    setCreateModalOpen(false);
  };

  const handleProjectCreated = () => {
    handleCreateProjectModalClose();
    loadProjects(1, 6); // Check it later
  };

  const handleViewDetails = (project: Project) => {
    message.info(`${t('viewDetails')}: ${project.title}`);
    console.log('Project details:', project);
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
        
        <div style={{ display: 'flex', gap: '8px' }}>
          <Button icon={<LeftOutlined />} />

          <InputNumber
            min={1}
            size="small"
          />

          <Button icon={<RightOutlined />} />
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