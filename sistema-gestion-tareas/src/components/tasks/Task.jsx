import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { API_URL, getToken } from "../../api/config";
import axios from "axios";
import { IoArrowBackSharp } from "react-icons/io5";
import { useTranslation } from "react-i18next";
import { getTaskById } from "../../api/getTaskById";

const Task = () => {
  const { t } = useTranslation();
  const { id } = useParams();
  const [task, setTask] = useState({});
  const [error, setError] = useState("");

  useEffect(() => {
    // const fetchTask = async () => {
    //   try {
    //     const response = await axios.get(`${API_URL}task/get-by-id/${id}`, {
    //       headers: {
    //         "Content-Type": "application/json",
    //         Authorization: `Bearer ${getToken()}`,
    //       },
    //     });

    //     if (response.status === 200) {
    //       setTask(response.data);
    //     } else {
    //       setError(t("error_response"));
    //     }
    //   } catch (error) {
    //     setError(error.response.data);
    //   }
    // };

    // fetchTask();

    getTaskById(id)
    .then((task) => {
      setTask(task);
    })
    .catch((err) => {
      setError(err.message);
    });
  }, []);

  return (
    <section className="h-full flex flex-col justify-center items-center p-5">
      <div className="bg-white w-full max-w-7xl rounded-lg overflow-x-auto border border-gray-200 p-2">
        <Link to="/tasks" className="text-2xl ">
          <IoArrowBackSharp />
        </Link>

        {error ? (
          <p className="text-md inline-block font-medium">{error}</p>
        ) : (
          <div className="p-10 min-w-[1000px]">
            {/* <div className="flex gap-x-6 items-center pb-2"> */}
            <p className="text-md inline-block font-medium bg-cyan-100 px-6 py-1 rounded-2xl">
              {task.status}
            </p>
            <h1 className="text-4xl font-bold my-2">{task.title}</h1>
            {/* </div> */}
            <h3 className="text-md text-gray-600">{task.expirationDate}</h3>
            <p className="py-8">{task.description}</p>
          </div>
        )}
      </div>
    </section>
  );
};

export default Task;
