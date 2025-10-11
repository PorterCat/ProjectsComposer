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
  const { t } = useTranslation();

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
      
      message.success(t('modal.createProject.success'));
      form.resetFields();
      onSuccess();
      
    } catch (error: any) {
      if (error.response?.status === 409) {
        message.warning(t('modal.createProject.conflict'));
      } else {
        message.error(t('modal.createProject.error'));
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
      title={t('modal.createProject.title')}
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
        initialValues={{
          leaderId: 'temp-leader-id'
        }}
      >
        <Flex gap="middle" vertical>
          <Form.Item
            label={t('modal.createProject.projectName')}
            name="title"
            rules={[
              { required: true, message: t('modal.createProject.projectNameRequired') },
              { min: 3, message: t('modal.createProject.projectNameMinError') }
            ]}
          >
            <Input 
              placeholder={t('modal.createProject.projectNamePlaceholder')} 
              size="large"
            />
          </Form.Item>

          <Flex gap="middle">
            <Form.Item
              label={t('modal.createProject.customerCompany')}
              name="customerCompanyName"
              rules={[{ required: true, message: t('modal.createProject.customerCompanyRequired') }]}
              style={{ flex: 1 }}
            >
              <Input 
                placeholder={t('modal.createProject.companyPlaceholder')} 
                size="large"
              />
            </Form.Item>

            <Form.Item
              label={t('modal.createProject.contractorCompany')}
              name="contractorCompanyName"
              rules={[{ required: true, message: t('modal.createProject.contractorCompanyRequired') }]}
              style={{ flex: 1 }}
            >
              <Input 
                placeholder={t('modal.createProject.companyPlaceholder')} 
                size="large"
              />
            </Form.Item>
          </Flex>

          <Flex gap="middle">
            <Form.Item
              label={t('modal.createProject.startDate')}
              name="startDate"
              rules={[{ required: true, message: t('modal.createProject.startDateRequired') }]}
              style={{ flex: 1 }}
            >
              <DatePicker 
                format="DD.MM.YYYY"
                placeholder={t('modal.createProject.selectDate')}
                style={{ width: '100%' }}
                size="large"
              />
            </Form.Item>

            <Form.Item
              label={t('modal.createProject.endDate')}
              name="endDate"
              style={{ flex: 1 }}
            >
              <DatePicker 
                format="DD.MM.YYYY"
                placeholder={t('modal.createProject.selectDate')}
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
                {t('modal.createProject.cancel')}
              </Button>
              <Button 
                type="primary" 
                htmlType="submit" 
                loading={loading}
                size="large"
              >
                {t('modal.createProject.create')}
              </Button>
            </Flex>
          </Form.Item>
        </Flex>
      </Form>
    </Modal>
  );
};