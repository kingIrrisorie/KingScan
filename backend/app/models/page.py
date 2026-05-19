from sqlalchemy import Column, Integer, ForeignKey
from sqlalchemy.orm import relationship

from app.database.connection import Base


class Page(Base):
    __tablename__ = "page"

    id = Column(Integer, primary_key=True)
    chapter_id = Column(Integer, ForeignKey("chapter.id"))
    page_number = Column(Integer, nullable=True)

    # RELAÇÕES

    # N:1 -> Page -> Chapter
    chapter = relationship(
        "Chapter",
        back_populates="pages"
    )

    # 1:N -> Page -> Images
    images = relationship(
        "Image",
        back_populates="page",
        cascade="all, delete-orphan"
    )