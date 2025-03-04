import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { validate } from "../../validations/validation";
import { getIdUser } from "../../api/config";

const TaskModal = ({
  isOpen,
  onClose,
  onSubmit,
  task,
  titleModal,
  titleButton,
  loadingMessage,
}) => {
  const { t } = useTranslation();
  const [taskData, setTaskData] = useState({
    userId: getIdUser(),
    description: "",
    expirationDate: new Date().toISOString().split("T")[0], // Formato YYYY-MM-DD
    title: "",
  });

  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState("");
  const [errors, setErrors] = useState({
    title: "",
    description: "",
    expirationDate: "",
  });

  useEffect(() => {
    if (task) {
      setTaskData({
        userId: task.userId,
        description: task.description || "",
        expirationDate: task.expirationDate || new Date().toISOString().split("T")[0],
        title: task.title || "",
      });
    } else {
      setTaskData({
        userId: getIdUser(),
        description: "",
        expirationDate: new Date().toISOString().split("T")[0],
        title: "",
      });
    }
    setErrors({ title: "", description: "", expirationDate: "" });
    setMessage("");
  }, [task, isOpen]);

  const handleChange = (e) => {
    const { name, value } = e.target;

    setMessage("");
    let errorMessage = validate(name, value);
    setErrors((prev) => ({ ...prev, [name]: errorMessage }));
    setTaskData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage("");

    try {
      await onSubmit(taskData);
      setMessage("Operación realizada con éxito.");
      setTimeout(() => {
        setLoading(false);
        onClose();
      }, 3000);
    } catch (error) {
      setMessage(error.response.data);
      console.log(error);
      setLoading(false);
    }
  };

  if (!isOpen) return null;

  return (
    <section
      className="fixed inset-0 flex items-center justify-center bg-black/50 z-50 p-4"
      onClick={(e) => e.target === e.currentTarget && onClose()}
    >
      <div className="w-full max-w-lg lg:max-w-[50%] bg-white border border-gray-300 rounded-lg shadow-lg p-8 max-h-[90vh] overflow-y-auto">
        <h3 className="text-2xl md:text-4xl pb-4 font-bold border-b border-gray-300">
          {titleModal}
        </h3>
        {!loading ? (
          <form onSubmit={handleSubmit} className="py-6">
            <div className="grid grid-cols-1 gap-4">
              <div>
                <label className="block font-semibold text-black">
                  {t("title")}
                </label>
                <input
                  type="text"
                  name="title"
                  value={taskData.title}
                  onChange={handleChange}
                  required
                  className="mt-2 block w-full h-12 rounded-md border border-gray-400 shadow-sm pl-3"
                />
                <div className="h-3 text-sm text-red-500">
                  {errors.title && (
                    <p>{errors.title}</p>
                  )}
                </div>
              </div>
              <div>
                <label className="block font-semibold text-black">
                  {t("description")}
                </label>
                <textarea
                  name="description"
                  value={taskData.description}
                  onChange={handleChange}
                  required
                  className="mt-2 block w-full h-28 rounded-md border border-gray-400 shadow-sm pl-3 pt-2"
                />
                <div className="h-3 text-sm text-red-500">
                  {errors.description && (
                    <p>
                      {errors.description}
                    </p>
                  )}
                </div>
              </div>
              <div>
                <label className="block font-semibold text-black">
                  {t("expiration_date")}
                </label>
                <input
                  type="date"
                  name="expirationDate"
                  value={taskData.expirationDate}
                  onChange={handleChange}
                  required
                  className="mt-2 block w-full h-12 rounded-md border border-gray-400 shadow-sm pl-3 pr-3"
                />
                <div className="h-3 text-sm text-red-500">
                  {errors.expirationDate && (
                    <p>
                      {errors.expirationDate}
                    </p>
                  )}
                </div>
              </div>
            </div>
            <div className="flex flex-col lg:flex-row justify-end gap-5 mt-6">
              <button
                type="button"
                onClick={onClose}
                className="w-full lg:w-auto lg:px-10 py-3 border border-gray-400 rounded-md bg-white hover:bg-gray-100 font-semibold text-black"
              >
                {" "}
                {t("cancel")}{" "}
              </button>
              <button
                type="submit"
                className="w-full lg:w-auto lg:px-10 py-3 border border-transparent rounded-md bg-black text-white hover:bg-gray-800 font-semibold"
              >
                {" "}
                {titleButton}{" "}
              </button>
            </div>
            {message && (
              <p className="mt-4 rounded-md bg-red-600 text-white px-4 py-2">
                {message}
              </p>
            )}
          </form>
        ) : (
          <p className="text-xl font-bold text-center py-4">{loadingMessage}</p>
        )}
      </div>
    </section>
  );
};

export default TaskModal;
