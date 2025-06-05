let mockTasks = [
  {
    id: 1,
    title: 'Completar el proyecto',
    description: 'Terminar la implementación del frontend',
    dueDate: '2024-03-30',
    priority: 'alta',
    completed: false
  },
  {
    id: 2,
    title: 'Reunión de equipo',
    description: 'Discutir el progreso del proyecto',
    dueDate: '2024-03-25',
    priority: 'media',
    completed: true
  }
];

let nextId = 3;

export const login = async (credentials) => {
  // Simular delay
  await new Promise(resolve => setTimeout(resolve, 500));

  if (credentials.email === 'test@example.com' && credentials.password === 'password') {
    return {
      token: 'mock-jwt-token',
      user: {
        id: 1,
        email: credentials.email,
        name: 'Usuario de Prueba'
      }
    };
  }
  throw new Error('Credenciales inválidas');
};

export const getTasks = async () => {
  await new Promise(resolve => setTimeout(resolve, 300));
  return [...mockTasks];
};

export const createTask = async (task) => {
  await new Promise(resolve => setTimeout(resolve, 300));
  const newTask = {
    ...task,
    id: nextId++,
    completed: false
  };
  mockTasks.push(newTask);
  return newTask;
};

export const updateTask = async (taskId, updatedTask) => {
  await new Promise(resolve => setTimeout(resolve, 300));
  const index = mockTasks.findIndex(task => task.id === taskId);
  if (index === -1) throw new Error('Tarea no encontrada');
  
  mockTasks[index] = {
    ...mockTasks[index],
    ...updatedTask,
    id: taskId
  };
  return mockTasks[index];
};

export const deleteTask = async (taskId) => {
  await new Promise(resolve => setTimeout(resolve, 300));
  const index = mockTasks.findIndex(task => task.id === taskId);
  if (index === -1) throw new Error('Tarea no encontrada');
  
  mockTasks = mockTasks.filter(task => task.id !== taskId);
  return { success: true };
};

export const toggleTaskStatus = async (taskId) => {
  await new Promise(resolve => setTimeout(resolve, 300));
  const index = mockTasks.findIndex(task => task.id === taskId);
  if (index === -1) throw new Error('Tarea no encontrada');
  
  mockTasks[index].completed = !mockTasks[index].completed;
  return mockTasks[index];
}; 