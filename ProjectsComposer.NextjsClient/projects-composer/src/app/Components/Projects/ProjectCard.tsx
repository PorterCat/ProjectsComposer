'use client';

import { Card, Button, Space, Typography } from 'antd';
import { Project } from '../../Models/Project';
import { useTranslation } from 'react-i18next';

const { Text } = Typography;

interface ProjectCardProps {
  project: Project;
  onViewDetails: (project: Project) => void;
}

export const ProjectCard = ({ project, onViewDetails }: ProjectCardProps) => {
  const { t } = useTranslation();

  return (
    <Card
      title={project.title}
      style={{ height: '100%' }}
      hoverable
      actions={[
        <Button 
          type="link" 
          key="view"
          onClick={() => onViewDetails(project)}
        >
          {t('projects.projectDetails')}
        </Button>,
      ]}
    >
      <Space direction="vertical" style={{ width: '100%' }}>
        <div>
          <Text strong>{t('projects.projectId')}: </Text>
          <Text type="secondary" style={{ fontSize: '12px' }}>{project.id}</Text>
        </div>
        <div>
          <Text strong>{t('projects.startDate')}: </Text>
          <Text>{project.startDate}</Text>
        </div>
        {project.endDate && (
          <div>
            <Text strong>{t('projects.endDate')}: </Text>
            <Text>{project.endDate}</Text>
          </div>
        )}
        {project.customerCompanyName && (
          <div>
            <Text strong>{t('projects.customer')}: </Text>
            <Text>{project.customerCompanyName}</Text>
          </div>
        )}
      </Space>
    </Card>
  );
};