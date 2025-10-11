'use client';

import { useTranslation } from 'react-i18next';
import { Button, Space } from 'antd';

const LanguageSwitcherButtons = () => {
  const { i18n } = useTranslation();

  const languages = {
    en: { name: 'English', flag: '🇺🇸' },
    ru: { name: 'Русский', flag: '🇷🇺' }
  };

  return (
    <Space>
      {Object.entries(languages).map(([code, lang]) => (
        <Button
          key={code}
          type={i18n.language === code ? 'primary' : 'default'}
          size="small"
          onClick={() => i18n.changeLanguage(code)}
        >
          <Space>
            <span style={{ fontSize: '14px' }}>{lang.flag}</span>
            <span>{lang.name}</span>
          </Space>
        </Button>
      ))}
    </Space>
  );
};

export default LanguageSwitcherButtons;