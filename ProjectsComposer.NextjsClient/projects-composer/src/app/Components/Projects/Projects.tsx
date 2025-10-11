'use client';

import { useEffect, useState } from 'react';
import { Button, Space, Typography, message } from 'antd';
import { Project } from '../../Models/Project';
import { projectService } from '../../Services/projectService';
import { PlusOutlined } from '@ant-design/icons';
import { ProjectList } from './ProjectList';
import { CreateProjectModal } from './CreateProjectModal';
import { useTranslation } from 'react-i18next';

const { Title } = Typography;

export const Projects = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const { t } = useTranslation();

  const loadProjects = async () => {
    try {
      setLoading(true);
      const data = await projectService.getAllProjects();
      setProjects(data);
    } catch (error) {
      message.error(t('projects.errorLoading'));
      console.error('Error loading projects:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadProjects();
  }, []);

  const showModal = () => {
    setModalOpen(true);
  };

  const handleModalClose = () => {
    setModalOpen(false);
  };

  const handleProjectCreated = () => {
    handleModalClose();
    loadProjects();
  };

  const handleViewDetails = (project: Project) => {
    message.info(`${t('projects.viewDetails')}: ${project.title}`);
    console.log('Project details:', project);
  };

  return (
    <div className="projects">
      <Space direction="vertical" style={{ width: '100%' }} size="large">
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <Title level={2}>{t('projects.title')}</Title>
          <Button 
            type="primary" 
            icon={<PlusOutlined />}
            onClick={showModal}
          >
            {t('projects.createProject')}
          </Button>
        </div>

        <ProjectList 
          projects={projects}
          loading={loading}
          onViewDetails={handleViewDetails}
        />
      </Space>

      <CreateProjectModal 
        open={modalOpen}
        onCancel={handleModalClose}
        onSuccess={handleProjectCreated}
      />
    </div>
  );
};