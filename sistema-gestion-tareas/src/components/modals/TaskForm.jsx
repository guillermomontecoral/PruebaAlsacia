import React from "react";
import { useTranslation } from "react-i18next";

const TaskForm = ({ task, errors, handleChange }) => {
  const { t } = useTranslation();
  return (
    <div className="grid grid-cols-1 gap-4">
      <div>
        <label className="block font-semibold text-black">{t("title")}</label>
        <input type="text" name="title" value={task.title} onChange={handleChange} className="mt-2 block w-full h-12 rounded-md border border-gray-400 shadow-sm pl-3" required />
        <div className="h-3 text-sm text-red-500">{errors.title && <p>{errors.title}</p>}</div>
      </div>
      <div>
        <label className="block font-semibold text-black">{t("description")}</label>
        <textarea name="description" value={task.description} onChange={handleChange} className="mt-2 block w-full h-28 rounded-md border border-gray-400 shadow-sm pl-3 pt-2" required />
        <div className="h-3 text-sm text-red-500">{errors.description && <p>{errors.description}</p>}</div>
      </div>
      <div>
        <label className="block font-semibold text-black">{t("expiration_date")}</label>
        <input type="date" name="expirationDate" value={task.expirationDate} onChange={handleChange} className="mt-2 block w-full h-12 rounded-md border border-gray-400 shadow-sm pl-3 pr-3" required />
        <div className="h-3 text-sm text-red-500">{errors.expirationDate && <p>{errors.expirationDate}</p>}</div>
      </div>
    </div>
  );
};

export default TaskForm;