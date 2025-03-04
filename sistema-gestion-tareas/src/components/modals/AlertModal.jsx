import React from "react";
import { useTranslation } from "react-i18next";

const AlertModal = ({
  isOpen,
  onClose,
  onConfirm,
  title = "Are you sure?",
}) => {
  const { t } = useTranslation();
  const handleConfirm = () => {
    onConfirm();
    onClose();
  };

  const handleCancel = () => {
    onClose();
  };

  if (!isOpen) return null;

  return (
    <section
      className="fixed inset-0 flex items-center justify-center bg-black/50 z-50 p-4"
      onClick={(e) => e.target === e.currentTarget && handleCancel()}
    >
      <div className="w-full max-w-lg bg-white border border-gray-300 rounded-lg shadow-lg p-8 max-h-[50vh] overflow-y-auto">
        <h3 className="text-xl font-bold text-center mb-4">{title}</h3>
        <div className="flex justify-around gap-4">
          <button
            onClick={handleConfirm}
            className="px-6 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-700"
          >
            {t("confirm")}
          </button>
          <button
            onClick={handleCancel}
            className="px-6 py-2 bg-gray-500 text-white rounded-md hover:bg-gray-700"
          >
            {t("cancel")}
          </button>
        </div>
      </div>
    </section>
  );
};

export default AlertModal;
