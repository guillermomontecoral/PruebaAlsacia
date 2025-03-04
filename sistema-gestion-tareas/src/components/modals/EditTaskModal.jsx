import axios from "axios";
import TaskModal from "./TaskModal";
import { API_URL, getToken } from "../../api/config";
import { useTranslation } from "react-i18next";

const EditTaskModal = ({ isOpen, onClose, task }) => {
  const { t } = useTranslation();
  const handleEditTask = async (taskData) => {
    await axios.put(`${API_URL}task/update/${task.id}`, taskData, {
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
      onSubmit={handleEditTask}
      task={task}
      titleModal={t("edit_task")}
      titleButton={t("edit_task")}
      loadingMessage={t("loadingEditMessage")}
    />
  );
};

export default EditTaskModal;
