import React, { useEffect, useRef, useState } from "react";
import { FaRegEdit, FaRegEye, FaRegTrashAlt } from "react-icons/fa";
import { Link } from "react-router-dom";
import { deleteTask } from "../../api/deleteTask";
import { getTasks } from "../../api/getTasks";
import { useTranslation } from "react-i18next";
import { IoAlertCircleOutline } from "react-icons/io5";
import { CiCircleCheck } from "react-icons/ci";
import { changeStatus } from "../../api/changeStatus";
import AddTaskModal from "../modals/AddTaskModal";
import EditTaskModal from "../modals/EditTaskModal";
import AlertModal from "../modals/AlertModal";

const Tasks = () => {
  const { t } = useTranslation();
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(false);
  const [isOpenNewTask, setIsOpenNewTask] = useState(false);
  const [isAlertOpen, setIsAlertOpen] = useState(false);
  const [taskDelete, setTaskDelete] = useState({});
  const [message, setMessage] = useState({
    success: "",
    error: "",
  });
  const [isOpenEditTask, setIsOpenEditTask] = useState(false);
  const [taskToEdit, setTaskToEdit] = useState(null);

  useEffect(() => {
      if(tasks.length === 0) {
        setLoading(true); // Activa el loading antes de hacer la petición
      }
      getTasks()
        .then((tasks) => {
          setTasks(tasks);
          setLoading(false); // Desactiva el loading después de obtener las tareas
        })
        .catch((err) => {
          setMessage({
            error: err.message,
          });
          setLoading(false); // Desactiva el loading también en caso de error
        });
  }, [isOpenNewTask, isOpenEditTask, isAlertOpen]);

  const handleDeleteTask = async (id) => {
    try {
      await deleteTask(id);
      setMessage({
        success: t("delete_task_sccess"),
      });
      setIsAlertOpen(false);
    } catch (error) {
      setMessage({
        error: error.message,
      });
    }
    setTimeout(() => {
      setMessage({
        success: "",
        error: "",
      });
    }, 3000);
  };

  const handleNewTask = () => {
    setTaskToEdit(null); // Limpiar la tarea en edición cuando agregamos una nueva
    setIsOpenNewTask(true);
  };

  const handleEditTask = (task) => {
    setTaskToEdit(task); // Asignamos la tarea seleccionada a `taskToEdit`
    setIsOpenEditTask(true);
  };

  const handleOpenModalDelete = (id) => {
    setTaskDelete(id);
    setIsAlertOpen(true);
  };

  const handleStatus = async (id, status) => {
    try {
      const result = await changeStatus(id, status);
      setTasks((prevTasks) => {
        const updatedTasks = prevTasks.map((task) =>
          task.id === id
            ? {
                ...task,
                status: status === "Created" ? "InProgress" : "Completed",
              }
            : task
        );
        return updatedTasks;
      });
      setMessage({
        success: result,
      });
    } catch (error) {
      setMessage({
        error: error.message,
      });
    }
    setTimeout(() => {
      setMessage({
        success: "",
        error: "",
      });
    }, 3000);
  };

  return (
    <section>
      <div className="flex justify-center items-center min-h-screen p-5 ">
        <div className="bg-white w-full max-w-7xl rounded-lg overflow-x-auto border border-gray-200 ">
          <div className="p-10 min-w-[1000px]">
            <div className="flex justify-between items-center">
              <div>
                <h2 className="text-4xl font-bold mb-5">{t("tasks")}</h2>
              </div>
              <button
                className="bg-black text-white px-8 py-3 rounded-md cursor-pointer"
                onClick={handleNewTask}
              >
                {t("add_task")}
              </button>
            </div>
            <div className="h-8">
              {message.success && (
                <p className="flex items-center gap-x-2 font-medium text-md text-green-600">
                  <CiCircleCheck className="text-2xl" />
                  {message.success}
                </p>
              )}
              {message.error && (
                <p className="flex items-center gap-x-2 font-medium text-md text-red-500">
                  <IoAlertCircleOutline className="text-2xl" />
                  {message.error}
                </p>
              )}
            </div>
          </div>
          {loading ? (
            <div className="flex justify-center items-center py-5 text-lg font-medium">
              <p>{t("get_tasks")}</p>
            </div>
          ) : tasks.length === 0 ? (
            <h3 className="text-lg font-medium mb-5 px-8">
              {t("no_registered_tasks")}
            </h3>
          ) : (
            <table className="w-full text-left min-w-[1000px]">
              <thead>
                <tr>
                  <th className="px-8 py-3 border-b border-gray-300 w-[20%]">
                    {t("title")}
                  </th>
                  <th className="px-8 py-3 border-b border-gray-300 w-[30%]">
                    {t("description")}
                  </th>
                  <th className="px-8 py-3 border-b border-gray-300 w-[20%]">
                    {t("expiration_date")}
                  </th>
                  <th className="px-8 py-3 border-b border-gray-300 w-[10%]">
                    {t("status")}
                  </th>
                  <th className="px-8 py-3 border-b border-gray-300 w-[10%]">
                    {t("actions")}
                  </th>
                </tr>
              </thead>
              <tbody>
                {tasks.map((task) => (
                  <tr key={task.id}>
                    <td className="px-8 py-5 text-gray-700 border-b border-gray-300 w-[20%]">
                      <Link
                        to={`/task/${task.id}`}
                        className="font-medium hover:text-blue-600"
                      >
                        {task.title}
                      </Link>
                    </td>
                    <td className="px-8 py-5 text-gray-700 border-b border-gray-300 max-w-xs truncate">
                      {task.description}
                    </td>
                    <td className="px-8 py-5 text-gray-700 border-b border-gray-300 w-[20%]">
                      {task.expirationDate}
                    </td>
                    <td className="px-8 py-5 text-gray-700 border-b border-gray-300 w-[10%]">
                      {task.status}
                    </td>
                    <td className="px-8 py-5 border-b border-gray-300 text-blue-600 w-[10%]">
                      <div className="flex gap-x-6 text-xl items-center justify-between">
                        <button
                          className={`text-xs px-2 py-1 rounded-2xl w-20 ${
                            task.status === "Completed"
                              ? "bg-gray-400 text-gray-700 cursor-not-allowed"
                              : "bg-black text-white cursor-pointer"
                          }`}
                          disabled={task.status === "Completed"}
                          onClick={() => handleStatus(task.id, task.status)}
                        >
                          {task.status === "Created"
                            ? `${t("start")}`
                            : `${t("finish")}`}
                        </button>
                        <Link to={`/task/${task.id}`}>
                          <FaRegEye />
                        </Link>
                        <button
                          disabled={task.status === "Completed"}
                          onClick={() => handleEditTask(task)}
                          className={`${
                            task.status === "Completed"
                              ? "text-gray-700 cursor-not-allowed"
                              : " text-green-500 hover:text-green-700 transition-all ease-in cursor-pointer"
                          }`}
                        >
                          <FaRegEdit />
                        </button>
                        <button
                          className="text-red-500 hover:text-red-700 transition-all ease-in cursor-pointer"
                          onClick={() => handleOpenModalDelete(task)}
                        >
                          <FaRegTrashAlt />
                        </button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </div>
      </div>
      <AddTaskModal
        isOpen={isOpenNewTask}
        onClose={() => setIsOpenNewTask(false)}
      />
      {taskToEdit && (
        <EditTaskModal
          isOpen={isOpenEditTask}
          onClose={() => setIsOpenEditTask(false)}
          task={taskToEdit}
        />
      )}
      <AlertModal
        isOpen={isAlertOpen}
        onClose={() => setIsAlertOpen(false)}
        onConfirm={() => handleDeleteTask(taskDelete.id)} // Pasamos la función correctamente
        title={`¿${t("question_delete")} ${taskDelete.title}?`}
      />
    </section>
  );
};

export default Tasks;
