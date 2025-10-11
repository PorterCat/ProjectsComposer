import i18next from "i18next";
import { initReactI18next } from "react-i18next";

import enTranslations from '../locales/en.json';
import ruTranslations from '../locales/ru.json';

const resources = {
  en: {
    translation: enTranslations
  },
  ru: {
    translation: ruTranslations
  }
};

i18next
  .use(initReactI18next)
  .init({
    resources,
    lng: 'ru', 
    fallbackLng: 'en',
    debug: process.env.NODE_ENV === 'development',
    interpolation: {
      escapeValue: false,
    }
  });

export default i18next;