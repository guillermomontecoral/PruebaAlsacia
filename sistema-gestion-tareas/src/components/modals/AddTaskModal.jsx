import axios from "axios";
import TaskModal from "./TaskModal";
import { API_URL, getToken } from "../../api/config";
import { useTranslation } from "react-i18next";

const AddTaskModal = ({ isOpen, onClose }) => {
  const { t } = useTranslation();
  const handleAddTask = async (taskData) => {
    await axios.post(`${API_URL}task/add-new`, taskData, {
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`,
      },
    });
  };

  return (
    <TaskModal
      isOpen={isOpen}
      onClose={onClose}
      onSubmit={handleAddTask}
      titleModal={t("add_new_task")}
      titleButton={t("add_btn")}
      loadingMessage={t("loadingAddMessage")}
    />
  );
};

export default AddTaskModal;
