import React, { useState } from 'react';
import { Button, Input, Modal, ModalOverlay, ModalContent, ModalHeader, ModalFooter, ModalBody, ModalCloseButton, Select } from "@chakra-ui/react";

const CreateProjectModal = ({ isOpen, onClose, onCreate, employees }) => {
    const [project, setProject] = useState({
        name: "",
        customerCompanyName: "",
        contractorCompanyName: "",
        priority: "",
        directorId: "",
        startDateTime: "",
        endDateTime: ""
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setProject({ ...project, [name]: value });
    };

    const handleSubmit = () => {
        onCreate(project);
        onClose();
    };

    return (
        <Modal isOpen={isOpen} onClose={onClose}>
            <ModalOverlay />
            <ModalContent>
                <ModalHeader>Create Project</ModalHeader>
                <ModalCloseButton />
                <ModalBody>
                    <Input
                        placeholder='Name'
                        name='name'
                        value={project.name}
                        onChange={handleChange}
                    />
                    <Input
                        placeholder='Customer Company Name'
                        name='customerCompanyName'
                        value={project.customerCompanyName}
                        onChange={handleChange}
                    />
                    <Input
                        placeholder='Contractor Company Name'
                        name='contractorCompanyName'
                        value={project.contractorCompanyName}
                        onChange={handleChange}
                    />
                    <Select
                        placeholder='Select a priority'
                        name='priority'
                        value={project.priority}
                        onChange={handleChange}
                    >
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                    </Select>
                    <Select
                        placeholder='Select a director'
                        name='directorId'
                        value={project.directorId}
                        onChange={handleChange}
                    >
                        {employees?.map((employee) => (
                            <option key={employee.id} value={employee.id}>{employee.email}</option>
                        ))}
                    </Select>
                    <Input
                        type="date"
                        placeholder='Start Date'
                        name='startDateTime'
                        value={project.startDateTime}
                        onChange={handleChange}
                    />
                    <Input
                        type="date"
                        placeholder='End Date'
                        name='endDateTime'
                        value={project.endDateTime}
                        onChange={handleChange}
                    />
                </ModalBody>
                <ModalFooter>
                    <Button colorScheme="blue" mr={3} onClick={onClose}>
                        Close
                    </Button>
                    <Button colorScheme="teal" onClick={handleSubmit}>
                        Create
                    </Button>
                </ModalFooter>
            </ModalContent>
        </Modal>
    );
};

export default CreateProjectModal;
