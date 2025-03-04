import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { API_URL, getToken } from "../../api/config";
import axios from "axios";
import { IoArrowBackSharp } from "react-icons/io5";

const Task = () => {
  const { id } = useParams();
  const [task, setTask] = useState({});

  useEffect(() => {
    const fetchTask = async () => {
      try {
        const response = await axios.get(`${API_URL}task/get-by-id/${id}`, {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${getToken()}`,
          },
        });

        if (response.status === 200) {
          setTask(response.data); // Usamos `response.data` para obtener los datos reales
        } else {
          console.error(`Unexpected response status: ${response.status}`);
        }
      } catch (error) {
        if (axios.isAxiosError(error)) {
          console.error("Axios error:", error.message);
        } else {
          console.error("Unexpected error:", error);
        }
      }
    };

    fetchTask();
  }, []);

  return (
    <section className="h-full flex flex-col justify-center items-center p-5">
        <div className="bg-white w-full max-w-7xl rounded-lg overflow-x-auto border border-gray-200 p-2">
          <Link to="/tasks" className="text-2xl ">
            <IoArrowBackSharp />
          </Link>

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
        </div>
    </section>
  );
};

export default Task;
