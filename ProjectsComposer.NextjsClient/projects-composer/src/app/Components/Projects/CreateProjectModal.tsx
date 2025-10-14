'use client';

import { Modal, Form, Input, DatePicker, Flex, Button, message } from 'antd';
import { CreateProjectRequest } from '../../Models/Project';
import { projectService } from "../../Services/projectService"
import dayjs from 'dayjs';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

interface CreateProjectModalProps {
  open: boolean;
  onCancel: () => void;
  onSuccess: () => void;
}

interface ProjectFormData {
  title: string;
  customerCompanyName: string;
  contractorCompanyName: string;
  startDate: dayjs.Dayjs;
  endDate?: dayjs.Dayjs;
  leaderId?: string;
}

export const CreateProjectModal = ({ open, onCancel, onSuccess }: CreateProjectModalProps) => {
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);
  const { t } = useTranslation('modal');

  const handleSubmit = async (values: ProjectFormData) => {
    try {
      setLoading(true);
      
      const projectData: CreateProjectRequest = {
        title: values.title,
        customerCompanyName: values.customerCompanyName,
        contractorCompanyName: values.contractorCompanyName,
        startDate: values.startDate.format('YYYY-MM-DD'),
        endDate: values.endDate ? values.endDate.format('YYYY-MM-DD') : undefined,
        leaderId: undefined
      };

      await projectService.createProject(projectData);
      
      message.success(t('createProject.success'));
      form.resetFields();
      onSuccess();
      
    } catch (error: any) {
      if (error.response?.status === 409) {
        message.warning(t('createProject.conflict'));
      } else {
        message.error(t('createProject.error'));
      }
      console.error('Error creating project:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleClose = () => {
    form.resetFields();
    onCancel();
  };

  return (
    <Modal
      title={t('createProject.title')}
      open={open}
      onCancel={handleClose}
      footer={null}
      width={600}
      styles={{
        body: { padding: '24px 0' }
      }}
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={handleSubmit}
        style={{ maxWidth: '100%' }}
      >
        <Flex gap="middle" vertical>
          <Form.Item
            label={t('createProject.projectName')}
            name="title"
            rules={[
              { required: true, message: t('createProject.projectNameRequired') },
              { min: 3, message: t('createProject.projectNameMinError') }
            ]}
          >
            <Input 
              placeholder={t('createProject.projectNamePlaceholder')} 
              size="large"
            />
          </Form.Item>

          <Flex gap="middle">
            <Form.Item
              label={t('createProject.customerCompany')}
              name="customerCompanyName"
              rules={[{ required: true, message: t('createProject.customerCompanyRequired') }]}
              style={{ flex: 1 }}
            >
              <Input 
                placeholder={t('createProject.companyPlaceholder')} 
                size="large"
              />
            </Form.Item>

            <Form.Item
              label={t('createProject.contractorCompany')}
              name="contractorCompanyName"
              rules={[{ required: true, message: t('createProject.contractorCompanyRequired') }]}
              style={{ flex: 1 }}
            >
              <Input 
                placeholder={t('createProject.companyPlaceholder')} 
                size="large"
              />
            </Form.Item>
          </Flex>

          <Flex gap="middle">
            <Form.Item
              label={t('createProject.startDate')}
              name="startDate"
              rules={[{ required: true, message: t('createProject.startDateRequired') }]}
              style={{ flex: 1 }}
            >
              <DatePicker 
                format="DD.MM.YYYY"
                placeholder={t('createProject.selectDate')}
                style={{ width: '100%' }}
                size="large"
              />
            </Form.Item>

            <Form.Item
              label={t('createProject.endDate')}
              name="endDate"
              style={{ flex: 1 }}
            >
              <DatePicker 
                format="DD.MM.YYYY"
                placeholder={t('createProject.selectDate')}
                style={{ width: '100%' }}
                size="large"
              />
            </Form.Item>
          </Flex>

          <Form.Item name="leaderId" hidden>
            <Input />
          </Form.Item>

          <Form.Item style={{ marginBottom: 0, marginTop: '16px' }}>
            <Flex gap="small" justify="flex-end">
              <Button onClick={handleClose} size="large">
                {t('createProject.cancel')}
              </Button>
              <Button 
                type="primary" 
                htmlType="submit" 
                loading={loading}
                size="large"
              >
                {t('createProject.create')}
              </Button>
            </Flex>
          </Form.Item>
        </Flex>
      </Form>
    </Modal>
  );
};